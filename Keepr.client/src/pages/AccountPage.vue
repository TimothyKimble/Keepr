<template>
  <div class="container-fluid p-1 m-3">
    <div class="row m-0 w-100 d-flex justify-content-center mt-4">
      <div class="col-md-1 col-1 p-0">
        <img class="img" :src="account.picture" alt="" :title="account.name" />
      </div>
      <div class="col-md-9 px-3 align-items-start d-flex flex-column justify-content-center text-break">
        <h3>{{ account.name }}</h3>
        <h6>Keeps: {{ keeps?.length }}</h6>
        <h6>Vaults: {{ vaults?.length }}</h6>
      </div>
    </div>
    <div class="row m-0 w-100">
      <div class="col-md-12 col-12 p-0 d-flex align-items-center">
        <h3 class="p-2">
          Vaults
        </h3>
        <i class="fas fa-plus-circle fa-2x p-2 AddIconColor grow" data-toggle="modal" data-target="#createVaultModal" title="Add Vault"></i>
      </div>
      <div class="card-columns ">
        <VaultsCard v-for="v in vaults" :key="v.id" :vault="v" />
      </div>
    </div>
    <div class="row m-0 w-100 mt-5">
      <div class="col-md-12 col-12 p-0 d-flex align-items-center">
        <h3 class="p-2">
          Keeps
        </h3>
        <i class="fas fa-plus-circle fa-2x p-2 AddIconColor grow" data-toggle="modal" data-target="#createKeepModal" title="Add Keep"></i>
      </div>
      <div class="card-columns">
        <KeepsCard v-for="k in keeps" :key="k.id" :keep="k" />
      </div>
    </div>
  </div>
  <AddKeepModal />
  <AddVaultModal />
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { accountService } from '../services/AccountService'
import Pop from '../utils/Notifier'
import { useRoute } from 'vue-router'
export default {
  name: 'Account',
  setup() {
    onMounted(async() => {
      try {
        await accountService.getAccountVaults(AppState.account.id)
        await accountService.getAccountKeeps(AppState.account.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.accountKeeps),
      vaults: computed(() => AppState.accountVaults)
    }
  }
}
</script>

<style scoped>
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
