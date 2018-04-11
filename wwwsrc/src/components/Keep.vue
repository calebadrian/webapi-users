<template>
    <div class="keep">
        <div class="image">
            <img :src="keep.pic">
            <div class="overlay d-flex flex-column justify-content-between">
                <div class="d-flex justify-content-between">
                    <button class="btn btn-primary" v-if="keep.userId == user.id" data-toggle="modal" :data-target="'#edit' + keep.id">
                        <i class="fas fa-edit"></i>
                    </button>
                    <button class="btn btn-danger" data-toggle="modal" :data-target="'#' + keep.id">Pin</button>
                </div>
                <div class="d-flex justify-content-between">
                    <button class="btn btn-info">Share</button>
                    <router-link :to="{name: 'SingleKeep', params: {keepId: keep.id}}">
                        <button class="btn btn-success" @click="increaseViews">View</button>
                    </router-link>
                </div>
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
                                    <div v-for="vault in userVaults" class="d-flex justify-content-between">
                                        <h5>{{vault.name}}</h5>
                                        <button class="btn btn-danger" @click="keepAtVault(vault)">Keep</button>
                                    </div>
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
            }
        },
        methods: {
            keepAtVault(vault) {
                this.$store.dispatch('addVaultKeep', {
                    vaultId: vault.id,
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
            deleteKeep(keep){
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

    .overlay {
        color: white;
        position: absolute;
        height: 69%;
        width: 100%;
        padding-left: 15px;
        padding-right: 15px;
        left: 0;
        top: 0;
        bottom: 0;
        right: 0;
        opacity: 0;
    }
</style>