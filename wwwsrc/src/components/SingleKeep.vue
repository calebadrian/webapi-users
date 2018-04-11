<template>
    <div class="singleKeep">
        <navbar></navbar>
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-12">
                    <img :src="keep.pic">
                    <h1>{{keep.name}}</h1>
                    <h5>{{keep.description}}</h5>
                    <h5>Author:
                        <router-link :to="{ name: 'Profile', params: {profileId: keepUser.id}}">{{keepUser.username}}</router-link>
                    </h5>
                    <social-sharing @open="increaseShares" :url="url" :title="keep.name" :description="keep.description"
                        :quote="keep.description" hashtags="vuejs,javascript,framework"
                        twitter-user="vuejs" inline-template>
                        <div>
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
                url: window.location.href
            }
        },
        mounted() {
            this.$store.dispatch('authenticate')
            this.$store.dispatch('getKeepById', this.$route.params.keepId)
        },
        methods: {
            increaseShares(){
                this.keep.shareCount++;
                this.$store.dispatch('editKeep', this.keep)
            }
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
            }
        }
    }
</script>

<style scoped>
</style>