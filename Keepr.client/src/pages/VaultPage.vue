<template>
  <div class="container-fluid p-1 m-3">
    <div class=" row m-0 w-100">
      <div class="col-md-12 p-0">
        <div class="row m-0 w-100 my-3 p-3">
          <div class="col-md-4 p-0">
            <h1>{{ vault.name }}</h1>
          </div>
          <div class="col-md-1 p-0 d-flex justify-content-center my-2" v-if="account.id === vault.creatorId">
            <i @click="deleteVault(vault.id)" class="fas fa-trash-alt red fa-2x"></i>
          </div>
          <div class="col-md-12 p-0">
            <h3>Keeps: {{ vaultKeeps?.length }}</h3>
          </div>
          <div class="card-columns">
            <VaultKeepsCard v-for="k in vaultKeeps" :key="k.id" :keep="k" />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core'
import { vaultKeepsService } from '../services/VaultKeepsService'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import Pop from '../utils/Notifier'
import { keepsService } from '../services/KeepsService'
import { router } from '../router'
export default {
  name: 'Component',
  setup() {
    const route = useRoute()
    onMounted(async() => {
      try {
        await vaultsService.getVaultById(route.params.id)
        await vaultKeepsService.getKeepsByVaultId(route.params.id)
      } catch (error) {
        router.push('/')
      }
    })
    return {
      async getKeepById(keepId) {
        try {
          await keepsService.getKeepById(keepId)
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      async deleteVault(id) {
        try {
          await vaultsService.deleteVault(id)
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      vaultKeeps: computed(() => AppState.vaultKeeps),
      vault: computed(() => AppState.activeVault),
      account: computed(() => AppState.account)
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
.red {
  color: red;
}
</style>
