import vue from 'vue'
import vuex from 'vuex'
import axios from 'axios'
import router from '../router'

var production = !window.location.host.includes('localhost')
var baseUrl = production ? '//keepr-c.herokuapp.com/' : '//localhost:5000/'

var ourAPI = axios.create({
    baseURL: baseUrl + 'api/',
    timeout: 5000,
    withCredentials: true
})

var auth = axios.create({
    baseURL: baseUrl + 'account/',
    timeout: 5000,
    withCredentials: true
})

vue.use(vuex)

export default new vuex.Store({
    state: {
        user: {},
        profileUser: {},
        userVaults: [],
        profileUserVaults: [],
        vault: {},
        keepsByVault: [],
        keeps: [],
        keep: {},
        keepUser: {},
        profileUserKeeps: [],
        vaultForKeep: {}
    },
    mutations: {
        setUser(state, payload) {
            state.user = payload;
        },
        setUserVaults(state, payload) {
            state.userVaults = payload;
        },
        setVault(state, payload) {
            state.vault = payload;
        },
        setKeepsByVault(state, payload) {
            state.keepsByVault = payload;
        },
        setKeeps(state, payload) {
            state.keeps = payload;
        },
        setKeep(state, payload) {
            state.keep = payload
        },
        setProfileUser(state, payload) {
            state.profileUser = payload
        },
        setProfileUserVaults(state, payload) {
            state.profileUserVaults = payload
        },
        setKeepUser(state, payload) {
            state.keepUser = payload
        },
        setProfileUserKeeps(state, payload) {
            state.profileUserKeeps = payload
        },
        setVaultForKeep(state, payload){
            state.vaultForKeep = payload
        }
    },
    actions: {
        login({ commit, dispatch }, payload) {
            auth.post('login', payload)
                .then(res => {
                    commit('setUser', res.data)
                    dispatch('getUserVaults', res.data.id)
                })
                .catch(err => {
                    console.error(err)
                })
        },
        register({ commit, dispatch }, payload) {
            auth.post('register', payload)
                .then(res => {
                    commit('setUser', res.data)
                    router.push({ name: "Profile", params: { profileId: res.data.id } })
                })
                .catch(err => {
                    console.error(err)
                })
        },
        authenticate({ commit, dispatch }, payload) {
            auth.get('authenticate')
                .then(res => {
                    commit('setUser', res.data)
                    if (res.data == "") {
                        router.push({ name: "Home" })
                    } else {
                        dispatch('getUserVaults', res.data.id)
                    }
                })
                .catch(err => {
                    console.error(err)
                })
        },
        logout({ commit, dispatch }, payload) {
            auth.delete('logout')
                .then(res => {
                    commit('setUser', {})
                    commit('setUserVaults', {})
                    router.push({ name: "Home" })
                })
                .catch(err => {
                    console.error(err)
                })
        },
        getKeepUser({ commit, dispatch }, payload) {
            auth.get('' + payload)
                .then(res => {
                    commit('setKeepUser', res.data)
                })
                .catch(err => {
                    console.error(err)
                })
        },
        getProfileUser({ commit, dispatch }, payload) {
            auth.get('' + payload)
                .then(res => {
                    commit('setProfileUser', res.data)
                })
                .catch(err => {
                    console.error(err)
                })
        },
        addVault({ commit, dispatch }, payload) {
            ourAPI.post('vaults', payload)
                .then(res => {
                    dispatch('getUserVaults', payload.userId)
                    dispatch('getProfileUserVaults', payload.userId)
                })
                .catch(err => {
                    console.error(err)
                })
        },
        getVault({ commit, dispatch }, payload) {
            ourAPI.get('vaults/' + payload)
                .then(res => {
                    commit('setVault', res.data)
                })
                .catch(err => {
                    console.error(err)
                })
        },
        deleteVault({ commit, dispatch }, payload) {
            ourAPI.delete('vaults/' + payload.id)
                .then(res => {
                    dispatch('getUserVaults', payload.userId)
                    dispatch('getProfileUserVaults', payload.userId)
                })
                .catch(err => {
                    console.error(err)
                })
        },
        editVault({ commit, dispatch }, payload) {
            ourAPI.put('vaults/' + payload.id, payload)
                .then(res => {
                    dispatch('getUserVaults', payload.userId)
                    dispatch('getProfileUserVaults', payload.userId)
                })
                .catch(err => {
                    console.error(err)
                })
        },
        removeFromVault({ commit, dispatch, state }, payload) {
            ourAPI.delete('vaultkeeps/' + payload)
                .then(res => {
                    dispatch('getKeepsByVault', state.vault.id)
                })
                .catch(err => {
                    console.error(err)
                })
        },
        getUserVaults({ commit, dispatch }, payload) {
            ourAPI.get('vaults/users/' + payload)
                .then(res => {
                    commit('setUserVaults', res.data)
                })
                .catch(err => {
                    console.error(err)
                })
        },
        getProfileUserVaults({ commit, dispatch }, payload) {
            ourAPI.get('vaults/users/' + payload)
                .then(res => {
                    commit('setProfileUserVaults', res.data)
                })
                .catch(err => {
                    console.error(err)
                })
        },
        addKeep({ commit, dispatch }, payload) {
            ourAPI.post('keeps', payload.newKeep)
                .then(res => {
                    if (res.data != '') {
                        dispatch('addVaultKeep', {
                            vaultId: payload.vault.id,
                            keepId: res.data.id,
                            userId: res.data.userId
                        })
                        dispatch('getProfileUserKeeps', res.data.userId)
                    }
                })
                .catch(err => {
                    console.error(err)
                })
        },
        editKeep({ commit, dispatch }, payload) {
            ourAPI.put('keeps', payload)
                .then(res => {
                    dispatch('getKeeps')
                })
                .catch(err => {
                    console.error(err)
                })
        },
        deleteKeep({ commit, dispatch }, payload) {
            ourAPI.delete('keeps/' + payload.id, payload)
                .then(res => {
                    dispatch('getKeeps')
                    dispatch('getProfileUserKeeps', payload.userId)
                })
                .catch(err => {
                    console.error(err)
                })
        },
        addVaultKeep({ commit, dispatch }, payload) {
            ourAPI.post('vaultkeeps', payload)
                .then(res => {
                    if (res.data != '') {
                        dispatch('getKeepsByVault', payload.vaultId)
                    }
                })
                .catch(err => {
                    console.error(err)
                })
        },
        getProfileUserKeeps({ commit, dispatch }, payload) {
            ourAPI.get('keeps/users/' + payload)
                .then(res => {
                    commit('setProfileUserKeeps', res.data)
                })
                .catch(err => {
                    console.error(err)
                })
        },
        getKeepsByVault({ commit, dispatch }, payload) {
            ourAPI.get('vaultkeeps/' + payload)
                .then(res => {
                    commit('setKeepsByVault', res.data)
                })
                .catch(err => {
                    console.error(err)
                })
        },
        getKeeps({ commit, dispatch }, payload) {
            ourAPI.get('keeps')
                .then(res => {
                    commit('setKeeps', res.data)
                })
                .catch(err => {
                    console.error(err)
                })
        },
        getKeepById({ commit, dispatch }, payload) {
            ourAPI.get('keeps/' + payload)
                .then(res => {
                    commit('setKeep', res.data)
                    dispatch('getKeepUser', res.data.userId)
                    dispatch('getVaultForKeep', res.data)
                })
                .catch(err => {
                    console.error(err)
                })
        },
        getVaultForKeep({ commit, dispatch }, payload) {
            ourAPI.get('vaultkeeps/' + payload.id + '/' + payload.userId)
                .then(res => {
                    commit('setVaultForKeep', res.data)
                })
                .catch(err => {
                    console.error(err)
                })
        }
    }
})