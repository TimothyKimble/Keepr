<template>
  <div class="component row m-0 w-100">
    <div class="col-md-12 p-0">
      <div class="row m-0 w-100 my-3">
        <div class="col-md-12 p-0 text-center my-2">
          <h1>{{ vault.name }}</h1>
        </div>
        <div class="col-md-12 p-0 d-flex justify-content-center my-2" v-if="account.id === vault.creatorId">
          <i @click="deleteVault(vault.id)" class="fas fa-trash-alt red fa-2x"></i>
        </div>
        <div class="card-columns">
          <VaultKeepsCard v-for="vk in vaultKeeps" :key="vk.id" :vault-keep="vk" />
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
export default {
  name: 'Component',
  setup() {
    const route = useRoute()
    onMounted(async() => {
      await vaultsService.getVaultById(route.params.id)
      await vaultKeepsService.getKeepsByVaultId(route.params.id)
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
