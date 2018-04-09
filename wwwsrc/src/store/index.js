import vue from 'vue'
import vuex from 'vuex'
import axios from 'axios'
import router from '../router'

var production = !window.location.host.includes('localhost')
var baseUrl = production ? '//something.herokuapp.com/' : '//localhost:5000/'

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
        userVaults: [],
        vault: {}
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
        }
    },
    actions: {
        login({ commit, dispatch }, payload) {
            auth.post('login', payload)
                .then(res => {
                    commit('setUser', res.data)
                })
                .catch(err => {
                    console.error(err)
                })
        },
        register({ commit, dispatch }, payload) {
            auth.post('register', payload)
                .then(res => {
                    commit('setUser', res.data)
                })
                .catch(err => {
                    console.error(err)
                })
        },
        authenticate({ commit, dispatch }, payload) {
            auth.get('authenticate', payload)
                .then(res => {
                    commit('setUser', res.data)
                    if (res.data == "") {
                        router.push({ name: "Home" })
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
                })
                .catch(err => {
                    console.error(err)
                })
        },
        addVault({ commit, dispatch }, payload) {
            ourAPI.post('vaults', payload)
                .then(res => {
                    dispatch('getUserVaults', payload.userId)
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
        getUserVaults({ commit, dispatch }, payload) {
            ourAPI.get('vaults/users/' + payload)
                .then(res => {
                    commit('setUserVaults', res.data)
                })
                .catch(err => {
                    console.error(err)
                })
        }
    }
})