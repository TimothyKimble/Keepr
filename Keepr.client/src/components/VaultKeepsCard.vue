<template>
  <div class="card">
    <img class="card-img" :src="vaultKeep.img" alt="">
    <div class="card-img-overlay d-flex justify-content-between align-items-end">
      <h4 class="card-title text-light text-shadow">
        {{ vaultKeep.name }}
      </h4>
      <!-- <router-link :to="{name: 'ProfilePage', params:{id: keep.creator.id}}"> -->
      <img class="rounded-circle profileImage" :src="vaultKeep.creator.picture" alt="">
      <!-- </router-link> -->
    </div>
  </div>
</template>

<script>
import { onMounted, reactive } from '@vue/runtime-core'
import { profilesService } from '../services/ProfilesService'
export default {
  name: 'Component',
  props: {
    vaultKeep: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
.text-shadow {
  text-shadow: black 1px 1px 1px;
}

.profileImage {
  width: 20%;
}

.grow {
transition: all .3s ease-in-out;
}
.grow:hover {
transform: scale(1.02);
cursor: pointer;
}
.rounded {
  border-radius: 10%;
}
</style>
