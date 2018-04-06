<template>
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
        <a class="navbar-brand" href="#">Navbar</a>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent"
            aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>

        <div class="collapse navbar-collapse" id="navbarSupportedContent">
            <div class="navBtn">
                <button class="btn btn-success" data-toggle="modal" data-target="#loginModal">Login</button>
                <button class="btn btn-primary" data-toggle="modal" data-target="#registerModal">Register</button>
            </div>
        </div>
        <div class="modal fade" id="loginModal" tabindex="-1" role="dialog">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Login</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <form @submit.prevent="login">
                            <input type="email" placeholder="email" v-model="loginUser.email">
                            <input type="password" placeholder="password" v-model="loginUser.password">
                            <button type="submit" class="btn btn-success">Login</button>
                        </form>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="registerModal" tabindex="-1" role="dialog">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Register</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <form @submit.prevent="register">
                            <input type="text" placeholder="username" v-model="newUser.username">
                            <input type="email" placeholder="email" v-model="newUser.email">
                            <input type="password" placeholder="password" v-model="newUser.password">
                            <button type="submit" class="btn btn-success">Register</button>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </nav>
</template>

<script>
    export default {
        name: 'Navbar',
        data() {
            return {
                loginUser: {
                    email: '',
                    password: ''
                },
                newUser: {
                    username: '',
                    email: '',
                    password: ''
                }
            }
        },
        methods: {
            login(){
                this.$store.dispatch('login', this.loginUser)
            },
            register(){
                this.$store.dispatch('register', this.newUser)
            }
        }
    }
</script>

<style scoped>
</style>