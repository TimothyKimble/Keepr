<template>
  <router-link :to="{name: 'VaultPage', params: {id: vault.id}}">
    <div class="component card grow">
      <img class="card-img" :src="vault.img" alt="">
      <div class="card-img-overlay d-flex justify-content-between align-items-end">
        <h4 class="card-title text-light text-shadow">
          {{ vault.name }}
        </h4>
        <img class="rounded-circle profileImage" :src="vault.creator.picture" alt="">
      </div>
    </div>
  </router-link>
</template>

<script>
import { AppState } from '../AppState'
import { router } from '../router'
export default {
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {
    }
  }
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

</style>
