<template>
    <div class="vault">
        <navbar></navbar>
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-12">
                    <h1>{{vault.name}}</h1>
                    <h6>Keeps: {{keeps.length}}</h6>
                </div>
                <div v-for="keep in keeps" class="col-md-2 col-sm-6" v-if="keep.private != 1 || keep.userId == user.id">
                    <keep :keep="keep"></keep>
                    <button class="btn btn-danger" @click="removeFromVault(keep)" v-if="keep.userId == user.id">Remove From Vault</button>
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
            },
            removeFromVault(keep) {
                this.$store.dispatch('removeFromVault', keep.vkId)
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