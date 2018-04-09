<template>
    <div class="profile">
        <navbar></navbar>
        <div class="container-fluid">
            <div class="row justify-content-between">
                <div class="col-sm-12 col-md-4">
                    <h1>{{user.username}}</h1>
                </div>
                <div class="col-sm-12 col-md-6">
                    <h1>My Vaults</h1>
                    <i class="fas fa-2x fa-plus-square" data-toggle="modal" data-target="#addVaultModal"></i>
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
                                    <form @submit.prevent="addVault">
                                        <input type="text" placeholder="name" v-model="newVault.name">
                                        <input type="text" placeholder="description" v-model="newVault.description">
                                        <button class="btn btn-success" type="submit">Add Vault</button>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 col-md-3" v-for="vault in userVaults">
                            <h4><router-link :to="{name: 'Vault', params: {vaultId: vault.id}}">{{vault.name}}</router-link></h4>
                            <h6>{{vault.description}}</h6>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import navbar from './Navbar'
    export default {
        name: 'Profile',
        data() {
            return {
                newVault: {
                    name: '',
                    description: '',
                    userId: ''
                }
            }
        },
        mounted() {
            this.$store.dispatch('authenticate')
            this.$store.dispatch('getUserVaults', this.$route.params.profileId)
        },
        methods: {
            addVault() {
                this.newVault.userId = this.user.id
                this.$store.dispatch('addVault', this.newVault)
            }
        },
        components: {
            navbar,
        },
        computed: {
            user() {
                return this.$store.state.user
            },
            userVaults() {
                return this.$store.state.userVaults
            }
        }
    }
</script>

<style scoped>
</style>