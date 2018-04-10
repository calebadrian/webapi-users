<template>
    <div class="keep">
        <h1 class="wrap">{{keep.name}}</h1>
        <h5>{{keep.description}}</h5>
        <h6>Keeps: {{keep.keepCount}}</h6>
        <h6>Shares: {{keep.shareCount}}</h6>
        <h6>Views: {{keep.viewCount}}</h6>
        <div class="overlay d-flex align-items-end justify-content-between">
            <button class="btn btn-info">Share</button>
            <button class="btn btn-danger" data-toggle="modal" :data-target="'#' + keep.id">Pin</button>
            <router-link :to="{name: 'SingleKeep', params: {keepId: keep.id}}">
                <button class="btn btn-success">View</button>
            </router-link>
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
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <button type="button" class="btn btn-primary">Save changes</button>
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
            }
        },
        computed: {
            userVaults() {
                return this.$store.state.userVaults
            }
        }
    }

</script>

<style scoped>
    .wrap {
        word-wrap: break-word;
    }

    .keep:hover .overlay {
        opacity: 1;
    }

    .overlay {
        border-radius: 10%;
        background: rgba(0, 0, 0, .2);
        color: white;
        position: absolute;
        height: 100%;
        width: 100%;
        left: 0;
        top: 0;
        bottom: 0;
        right: 0;
        opacity: 0;
    }
</style>