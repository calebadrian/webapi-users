// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
//@ts-ignore
import App from './App'
import router from './router'
import store from './store'
import vSelect from 'vue-select'
import SocialSharing from 'vue-social-sharing'
import Popover from 'vue-js-popover'

Vue.config.productionTip = false
Vue.component('v-select', vSelect);
Vue.component('social-sharing', SocialSharing)
Vue.use(Popover);

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store,
  components: { App },
  template: '<App/>'
})
