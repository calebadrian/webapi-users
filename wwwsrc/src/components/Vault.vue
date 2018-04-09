<template>
    <div class="vault">
        <navbar></navbar>
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-12">
                    <h1>{{vault.name}}</h1>
                    <i class="fas fa-2x fa-plus-square" data-toggle="modal" data-target="#addKeepModal"></i>
                </div>
                <div v-for="keep in keeps" class="col-md-2 col-sm-6">
                    <keep :keep="keep"></keep>
                </div>
            </div>
        </div>
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
                        <form @submit.prevent="addKeep">
                            <input type="text" placeholder="name" v-model="newKeep.name">
                            <input type="text" placeholder="description" v-model="newKeep.description">
                            <button class="btn btn-success" type="submit">Create Keep</button>
                        </form>
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
        name: 'Vault',
        data() {
            return {
                newKeep: {
                    name: '',
                    description: ''
                }
            }
        },
        mounted() {
            this.$store.dispatch('authenticate')
            this.$store.dispatch('getVault', this.$route.params.vaultId)
            this.$store.dispatch('getKeepsByVault', this.$route.params.vaultId)
        },
        methods: {
            addKeep() {
                this.newKeep.userId = this.user.id;
                this.$store.dispatch('addKeep', this.newKeep)
            }
        },
        components: {
            navbar,
            keep
        },
        computed: {
            vault() {
                return this.$store.state.vault;
            },
            user() {
                return this.$store.state.user
            },
            keeps() {
                return this.$store.state.keepsByVault
            }
        }
    }
</script>

<style scoped>
</style>