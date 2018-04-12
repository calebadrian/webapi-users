<template>
    <div class="singleKeep">
        <navbar></navbar>
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-12 d-flex justify-content-end">
                    <popover name="pop">
                        <social-sharing @open="increaseShares" :url="url" :title="keep.name" :description="keep.description" :quote="keep.description"
                            hashtags="vuejs,javascript,framework" twitter-user="vuejs" inline-template>
                            <div class="d-flex flex-column test">
                                <network network="email">
                                    <i class="fa fa-envelope"></i> Email
                                </network>
                                <network network="facebook">
                                    <i class="fab fa-facebook"></i> Facebook
                                </network>
                                <network network="linkedin">
                                    <i class="fab fa-linkedin"></i> LinkedIn
                                </network>
                                <network network="reddit">
                                    <i class="fab fa-reddit-square"></i> Reddit
                                </network>
                                <network network="twitter">
                                    <i class="fab fa-twitter-square"></i> Twitter
                                </network>
                            </div>
                        </social-sharing>
                    </popover>
                    <button class="btn btn-info" v-popover:pop.bottom><i class="far fa-share-square"></i></button>
                    <button class="btn btn-danger ml-1" data-toggle="modal" :data-target="'#' + keep.id"><i class="fas fa-thumbtack"></i></button>
                    <div class="modal fade" :id="keep.id" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="exampleModalLabel">Choose Vault</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="container-fluid">
                                        <div class="row align-items-center">
                                            <div class="col-sm-6">
                                                <h3>{{keep.name}}</h3>
                                            </div>
                                            <div class="col-sm-6">
                                                <form @submit.prevent="keepAtVault">
                                                    <v-select label="name" :options="userVaults" v-model="vault"></v-select>
                                                    <button class="btn btn-success">Keep</button>
                                                </form>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <img :src="keep.pic">
                </div>
                <div class="col-sm-6">
                    <h1>{{keep.name}}</h1>
                    <h5>{{keep.description}}</h5>
                    <router-link class="d-flex justify-content-start align-items-center" :to="{ name: 'Profile', params: {profileId: keepUser.id}}">
                        <img class="profilePic" :src="keepUser.profilePic" v-if="keepUser.profilePic">
                        <img class="profilePic" src="http://placehold.it/50x50" v-else>
                        <h5 class="ml-2">{{keepUser.username}}</h5>
                    </router-link>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import navbar from './Navbar'
    export default {
        name: 'Keep',
        data() {
            return {
                url: window.location.href,
                vault: ''
            }
        },
        mounted() {
            this.$store.dispatch('authenticate')
            this.$store.dispatch('getKeepById', this.$route.params.keepId)
        },
        methods: {
            increaseShares() {
                this.keep.shareCount++;
                this.$store.dispatch('editKeep', this.keep)
            },
            keepAtVault() {
                this.$store.dispatch('addVaultKeep', {
                    vaultId: this.vault.id,
                    keepId: this.keep.id,
                    userId: this.$store.state.user.id
                })
                this.keep.keepCount++
                this.$store.dispatch('editKeep', this.keep)
            },
        },
        components: {
            navbar
        },
        computed: {
            keep() {
                return this.$store.state.keep
            },
            keepUser() {
                return this.$store.state.keepUser
            },
            userVaults() {
                return this.$store.state.userVaults
            }
        }
    }
</script>

<style scoped>
    .profilePic {
        width: 50px;
        height: 50px;
        border-radius: 100%;
    }

    .vue-popover {
        top: 80% !important;
        width: 10% !important;
    }

    div[data-popover="pop"] {
        background: #444;
        color: #f9f9f9;

        font-size: 15px;
        line-height: 1.5;
        margin: 5px;
    }
</style>