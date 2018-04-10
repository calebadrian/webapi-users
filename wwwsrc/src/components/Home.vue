<template>
  <div class="home">
    <navbar></navbar>
    <div class="container-fluid">
      <div class="row">
        <div class="col-md-2 col-sm-6" v-for="keep in keeps">
          <keep :keep="keep" v-if="keep.private != 1"></keep>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import navbar from './Navbar'
  import keep from './Keep'
  export default {
    name: 'Home',
    data() {
      return {

      }
    },
    mounted() {
      this.$store.dispatch('authenticate')
      this.$store.dispatch('getKeeps')
    },
    components: {
      navbar,
      keep
    },
    computed: {
      keeps() {
        return this.$store.state.keeps
      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>