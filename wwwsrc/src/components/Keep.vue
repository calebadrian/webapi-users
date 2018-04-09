<template>
    <div class="keep">
        <h1 class="wrap">{{keep.name}}</h1>
        <h5>{{keep.description}}</h5>
        <div class="overlay d-flex align-items-end justify-content-between">
            <button class="btn btn-info">Share</button>
            <button class="btn btn-danger">Pin</button>
            <router-link :to="{name: 'SingleKeep', params: {keepId: keep.id}}"><button class="btn btn-success">View</button></router-link>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'Keep',
        props: ['keep'],
        data() {
            return {
            }
        },
    }

</script>

<style scoped>
    .wrap{
        word-wrap: break-word;
    }
    .keep:hover .overlay{
        opacity: 1;
    }
    .overlay{
        border-radius: 10%;
        background: rgba(0, 0, 0, .2);
        color: white;
        position: absolute;
        height: 100%;
        width: 100%;
        left: 0;
        top: 0;
        bottom: 0;
        right: 0;
        opacity: 0;
    }
</style>