<template>
    <div class="vault">
        <navbar></navbar>
        <h1>{{vault.name}}</h1>
    </div>
</template>

<script>
    import navbar from './Navbar'
    export default {
        name: 'Vault',
        data() {
            return {
            }
        },
        mounted(){
            this.$store.dispatch('authenticate')
            this.$store.dispatch('getVault', this.$route.params.vaultId)
        },
        methods: {
        },
        components: {
            navbar
        },
        computed: {
            vault(){
                return this.$store.state.vault;
            }
        }
    }
</script>

<style scoped>
</style>