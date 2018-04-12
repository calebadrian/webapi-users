import Vue from 'vue'
import Router from 'vue-router'
//@ts-ignore
import Home from '@/components/Home'
//@ts-ignore
import Profile from '@/components/Profile'
//@ts-ignore
import Vault from '@/components/Vault'
//@ts-ignore
import SingleKeep from '@/components/SingleKeep'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Home',
      component: Home
    },
    {
      path: '/profile/:profileId',
      name: 'Profile',
      component: Profile
    },
    {
      path: '/vaults/:vaultId',
      name: 'Vault',
      component: Vault
    },
    {
      path: '/keeps/:keepId',
      name: 'SingleKeep',
      component: SingleKeep
    }
  ]
})
