<template>
    <div class="profile">
        <navbar></navbar>
        <div class="container-fluid">
            <div class="row justify-content-between">
                <div class="col-sm-12">
                    <div class="row profile-card">
                        <div class="col-sm-12 col-md-6">
                            <h1>{{profileUser.username}}</h1>
                            <h6>Vaults: {{profileUserVaults.length}}</h6>
                            <h6>Keeps: {{profileUserKeeps.length}}</h6>
                        </div>
                        <div class="col-sm-12 col-md-6">
                            <div v-if="profileUser.profilePic">
                                <img :src="profileUser.profilePic">
                            </div>
                            <div v-else>
                                <img src="http://placehold.it/200x200">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row justify-content-center">
                <div class="col-sm-10">
                    <ul class="nav nav-pills mb-3" id="pills-tab" role="tablist">
                        <li class="nav-item">
                            <a class="nav-link active" id="pills-vaults-tab" data-toggle="pill" href="#pills-vaults" role="tab">Vaults</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="pills-keeps-tab" data-toggle="pill" href="#pills-keeps" role="tab">Keeps</a>
                        </li>
                    </ul>
                    <div class="tab-content" id="pills-tabContent">
                        <div class="tab-pane fade show active" id="pills-vaults" role="tabpanel">
                            <i class="fas fa-2x fa-plus-square mb-4" data-toggle="modal" data-target="#addVaultModal" v-if="profileUser.id == user.id"></i>
                            <div class="modal fade" id="addVaultModal" tabindex="-1" role="dialog">
                                <div class="modal-dialog" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title" id="exampleModalLabel">Add New Vault</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <form @submit.prevent="addVault" class="d-flex flex-column">
                                                <input type="text" placeholder="name" v-model="newVault.name">
                                                <input type="text" placeholder="description" v-model="newVault.description">
                                                <div>
                                                    <label for="private">
                                                        private
                                                    </label>
                                                    <input type="checkbox" name="private" v-model="newVault.private">
                                                </div>
                                                <button class="btn btn-success" type="submit">Add Vault</button>
                                            </form>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row justify-content-center">
                                <div class="col-sm-12 col-md-3 d-flex flex-column align-items-center" v-for="vault in profileUserVaults">
                                    <div v-if="vault.private != 1 || profileUser.id == user.id">
                                        <h4>
                                            <router-link :to="{name: 'Vault', params: {vaultId: vault.id}}">{{vault.name}}</router-link>
                                        </h4>
                                        <h6>{{vault.description}}</h6>
                                        <button class="btn btn-danger" @click="deleteVault(vault)" v-if="profileUser.id == user.id">Delete</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="pills-keeps" role="tabpanel">
                            <i class="fas fa-2x fa-plus-square" data-toggle="modal" data-target="#addKeepModal" v-if="profileUser.id == user.id"></i>
                            <div class="modal fade" id="addKeepModal" tabindex="-1" role="dialog">
                                <div class="modal-dialog" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title" id="exampleModalLabel">Add New Keep</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <form @submit.prevent="addKeep" class="d-flex flex-column">
                                                <input type="text" placeholder="name" v-model="newKeep.name">
                                                <input type="text" placeholder="description" v-model="newKeep.description">
                                                <input type="url" placeholder="image" v-model="newKeep.pic">
                                                <div>
                                                    <label for="private">
                                                        private
                                                    </label>
                                                    <input type="checkbox" name="private" v-model="newKeep.private">
                                                </div>
                                                <v-select label="name" :options="profileUserVaults" v-model="vault"></v-select>
                                                <button class="btn btn-success" type="submit">Create Keep</button>
                                            </form>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row justify-content-center">
                                <div class="col-sm-12 col-md-3 d-flex flex-column align-items-center" v-for="keep in profileUserKeeps">
                                    <keep :keep="keep"></keep>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import navbar from './Navbar'
    import keep from './Keep'
    export default {
        name: 'Profile',
        data() {
            return {
                newVault: {
                    name: '',
                    description: '',
                    private: false,
                    userId: ''
                },
                newKeep: {
                    name: '',
                    description: '',
                    pic: '',
                    private: false,
                    keepCount: 0,
                    shareCount: 0,
                    viewCount: 0
                },
                vault: ''
            }
        },
        mounted() {
            this.$store.dispatch('authenticate')
            this.$store.dispatch('getProfileUser', this.$route.params.profileId)
            this.$store.dispatch('getProfileUserVaults', this.$route.params.profileId)
            this.$store.dispatch('getProfileUserKeeps', this.$route.params.profileId)
        },
        methods: {
            addVault() {
                this.newVault.userId = this.user.id
                if (!this.newVault.private) {
                    this.newVault.private = 0
                } else {
                    this.newVault.private = 1
                }
                this.$store.dispatch('addVault', this.newVault)
            },
            addKeep() {
                if (!this.newKeep.private) {
                    this.newKeep.private = 0
                } else {
                    this.newKeep.private = 1
                }
                this.newKeep.userId = this.user.id;
                this.$store.dispatch('addKeep', { newKeep: this.newKeep, vault: this.vault })
            },
            deleteVault(vault) {
                this.$store.dispatch('deleteVault', vault);
            }
        },
        components: {
            navbar,
            keep
        },
        computed: {
            user() {
                return this.$store.state.user
            },
            profileUser() {
                return this.$store.state.profileUser
            },
            profileUserVaults() {
                return this.$store.state.profileUserVaults
            },
            profileUserKeeps() {
                return this.$store.state.profileUserKeeps
            }
        }
    }
</script>

<style scoped>
    img {
        width: 200px;
        height: 200px;
        border-radius: 100%;
    }

    .container-fluid {
        margin-top: 3vh;
    }
</style>