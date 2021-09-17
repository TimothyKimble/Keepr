<template>
  <div class="container-fluid p-1 m-3 ProfilePage">
    <div class="row m-0 w-100 d-flex justify-content-center mt-4">
      <div class="col-md-1 col-1 p-0">
        <img class="img" :src="profile.picture" alt="" :title="profile.name" />
      </div>
      <div class="col-md-9 px-3 align-items-start d-flex flex-column justify-content-center">
        <h3>{{ profile.name }}</h3>
        <h6>Keeps: {{ keeps.length }}</h6>
        <h6>Vaults: {{ vaults.length }}</h6>
      </div>
    </div>
    <div class="row m-0 w-100">
      <div class="col-md-12 col-12 p-0 d-flex align-items-center">
        <h3 class="p-2">
          Vaults
        </h3>
      </div>
      <div class="card-columns">
        <VaultsCard v-for="v in vaults.filter(v => v.isPrivate !== true) " :key="v.id" :vault="v" />
      </div>
    </div>
    <div class="row m-0 w-100 mt-5">
      <div class="col-md-12 col-12 p-0 d-flex align-items-center">
        <h3 class="p-2">
          Keeps
        </h3>
      </div>
      <div class="card-columns">
        <KeepsCard v-for="k in keeps" :key="k.id" :keep="k" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { profilesService } from '../services/ProfilesService'
import Pop from '../utils/Notifier'
import { useRoute } from 'vue-router'
export default {
  name: 'ProfilePage',
  setup() {
    const route = useRoute()
    onMounted(async() => {
      try {
        await profilesService.getProfile(route.params.id)
        await profilesService.getProfileKeeps(AppState.activeProfile.id)
        await profilesService.getProfileVaults(AppState.activeProfile.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      profile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults)
    }
  }
}
</script>

<style lang="scss" scoped>
img {
  width: 100%;
}

.AddIconColor {
  color: #27ae60;
}

.grow {
transition: all .3s ease-in-out;
}
.grow:hover {
transform: scale(1.05);
cursor: pointer;
}
</style>
