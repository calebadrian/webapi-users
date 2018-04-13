<template>
    <div class="keep">
        <div class="image">
            <img :src="keep.pic">
            <div class="overlay d-flex justify-content-between">
                <button class="btn btn-primary" v-if="keep.userId == user.id" data-toggle="modal" :data-target="'#edit' + keep.id">
                    <i class="fas fa-edit"></i>
                </button>
                <button class="btn btn-danger" data-toggle="modal" :data-target="'#' + keep.id" v-if="user.id">
                    <i class="fas fa-thumbtack"></i>
                </button>
                <router-link :to="{name: 'SingleKeep', params: {keepId: keep.id}}" v-if="user.id">
                    <button class="btn btn-success" @click="increaseViews">
                        <i class="fas fa-eye"></i>
                    </button>
                </router-link>
                <button v-else class="btn hidden">Hide</button>
                <popover name="pop">
                    <social-sharing @open="increaseShares" :url="url" :title="keep.name" :description="keep.description" :quote="keep.description" inline-template>
                        <div class="d-flex flex-column">
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
                <button class="btn btn-info" v-popover:pop.bottom>
                    <i class="far fa-share-square"></i>
                </button>
            </div>
        </div>
        <h1 class="wrap">{{keep.name}}</h1>
        <div class="d-flex justify-content-between">
            <h6>
                <i class="fab fa-kickstarter-k"></i>: {{keep.keepCount}}</h6>
            <h6>
                <i class="fas fa-share"></i>: {{keep.shareCount}}</h6>
            <h6>
                <i class="fas fa-eye"></i>: {{keep.viewCount}}</h6>
        </div>
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
        <div class="modal fade" :id="'edit' + keep.id" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Edit Keep</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <form @submit.prevent="editKeep(keep)" class="d-flex flex-column">
                            <input type="text" v-model="keep.name">
                            <input type="text" v-model="keep.description">
                            <input type="url" v-model="keep.pic">
                            <div>
                                <label for="private">
                                    private
                                </label>
                                <input type="checkbox" name="private" v-model="keep.private">
                            </div>
                            <button type="submit" class="btn btn-success">Edit Keep</button>
                        </form>
                        <button class="btn btn-danger" @click="deleteKeep(keep)" v-if="keep.private == 1 && keep.userId == user.id" data-dismiss="modal">Delete</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'Keep',
        props: ['keep'],
        data() {
            return {
                url: window.location.href + 'keeps/' + this.keep.id,
                vault: ''
            }
        },
        methods: {
            keepAtVault() {
                this.$store.dispatch('addVaultKeep', {
                    vaultId: this.vault.id,
                    keepId: this.keep.id,
                    userId: this.$store.state.user.id
                })
                this.keep.keepCount++
                this.$store.dispatch('editKeep', this.keep)
            },
            editKeep(keep) {
                if (!keep.private) {
                    keep.private = 0
                } else {
                    keep.private = 1
                }
                this.$store.dispatch('editKeep', keep)
            },
            deleteKeep(keep) {
                if (!keep.private) {
                    keep.private = 0
                } else {
                    keep.private = 1
                }
                this.$store.dispatch('deleteKeep', keep)
            },
            increaseViews() {
                this.keep.viewCount++
                this.$store.dispatch('editKeep', this.keep)
            },
            increaseShares() {
                this.keep.shareCount++;
                this.$store.dispatch('editKeep', this.keep)
            }
        },
        computed: {
            userVaults() {
                return this.$store.state.userVaults
            },
            profileUser() {
                return this.$store.state.profileUser
            },
            user() {
                return this.$store.state.user
            }
        }
    }

</script>

<style scoped>
    .wrap {
        word-wrap: break-word;
    }

    img {
        width: 100%;
    }

    .image:hover .overlay {
        opacity: 1;
    }

    .image:hover img {
        opacity: .6;
    }

    .hidden{
        opacity: 0;
        pointer-events: none;
    }

    .overlay {
        position: absolute;
        height: 10%;
        width: 100%;
        padding-left: 15px;
        padding-right: 15px;
        left: 0;
        top: 0;
        bottom: 0;
        right: 0;
        opacity: 0;
    }

    .overlay button{
        height: 100%;
    }

    .vue-popover {
        left: 55% !important;
        top: 86% !important;
        width: 50% !important;
    }

    div[data-popover="pop"] {
        background: #444;
        color: #f9f9f9;

        font-size: 15px;
        line-height: 1.5;
        margin: 5px;
    }
</style>