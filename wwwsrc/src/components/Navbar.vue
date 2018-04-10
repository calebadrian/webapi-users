<template>
    <div class="navbar">
        <nav class="navbar fixed-top navbar-expand-lg navbar-dark bg-dark">
            <a class="navbar-brand" href="/">Navbar</a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent"
                aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>

            <div class="collapse navbar-collapse" id="navbarSupportedContent">
                <ul class="navbar-nav mr-auto">
                    <li class="nav-item dropdown">
                        <a class="nav-link dropdown-toggle" href="/#/" id="navbarDropdown" role="button" data-toggle="dropdown">
                            Pages
                        </a>
                        <div class="dropdown-menu">
                            <router-link :to="{name: 'Profile', params: {profileId: user.id}}" v-if="user.id">
                                <a class="dropdown-item">My Profile</a>
                            </router-link>
                        </div>
                    </li>
                </ul>
                <div v-if="user.id" class="d-flex align-items-center">
                    <h4 class="mr-2">{{user.username}}</h4>
                    <div class="navBtn">
                        <button class="btn btn-danger" @click="logout">Logout</button>
                    </div>
                </div>
                <div class="navBtn" v-else>
                    <button class="btn btn-success" data-toggle="modal" data-target="#loginModal">Login</button>
                    <button class="btn btn-primary" data-toggle="modal" data-target="#registerModal">Register</button>
                </div>
            </div>
        </nav>
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
                        <form @submit.prevent="register" class="d-flex flex-column">
                            <input type="text" placeholder="username" v-model="newUser.username">
                            <input type="email" placeholder="email" v-model="newUser.email">
                            <input type="url" placeholder="profile picture" v-model="newUser.profilePic">
                            <input type="password" placeholder="password" v-model="newUser.password">
                            <button type="submit" class="btn btn-success">Register</button>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
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
                    profilePic: '',
                    password: ''
                }
            }
        },
        methods: {
            login() {
                this.$store.dispatch('login', this.loginUser)
            },
            register() {
                this.$store.dispatch('register', this.newUser)
            },
            logout() {
                this.$store.dispatch('logout')
            }
        },
        computed: {
            user() {
                return this.$store.state.user
            }
        }
    }
</script>

<style scoped>
    .navbar {
        height: 10vh;
    }
    h4{
        color: white;
        margin-bottom: 0;
    }
</style>