<template>
    <div class="singleKeep">
        <navbar></navbar>
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-12">
                    <h1>{{keep.name}}</h1>
                    <h5>{{keep.description}}</h5>
                    <h5>{{keepUser.username}}</h5>
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
            }
        },
        mounted() {
            this.$store.dispatch('authenticate')
            this.$store.dispatch('getKeepById', this.$route.params.keepId)
        },
        components: {
            navbar
        },
        computed: {
            keep(){
                return this.$store.state.keep
            },
            keepUser(){
                return this.$store.state.keepUser
            }
        }
    }
</script>

<style scoped>

</style>