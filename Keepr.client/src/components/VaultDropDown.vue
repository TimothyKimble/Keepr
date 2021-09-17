<template>
  <div class="row m-0 w-100">
    <div class="col-md-12 p-0 text-break">
      <button @click="createVaultKeep(vault)" class="dropdown-item text-break p-0">
        <p class="m-0 text-break">
          {{ vault.name }}
        </p>
      </button>
    </div>
  </div>
</template>

<script>
import { reactive } from '@vue/runtime-core'
import { AppState } from '../AppState'
import Pop from '../utils/Notifier'
import { vaultKeepsService } from '../services/VaultKeepsService'
import { keepsService } from '../services/KeepsService'
export default {
  name: 'Component',
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup() {
    const state = reactive({
      newVaultKeep: {}
    })
    return {
      state,
      async createVaultKeep(vault) {
        try {
          state.newVaultKeep.vaultId = vault.id
          state.newVaultKeep.keepId = AppState.activeKeep.id
          const keep = await keepsService.getKeepById(state.newVaultKeep.keepId)
          await vaultKeepsService.createVaultKeep(state.newVaultKeep, keep)
          state.newVaultKeep = {}
          AppState.activeKeep.keeps++
          Pop.toast('Created VaultKeep', 'success')
        } catch (error) {
          Pop.toast('Did not create vaultKeep', 'error')
        }
      },
      async increaseKeeps() {
        try {
          const keep = await keepsService.getKeepById(state.newVaultKeep.keepId)
          await vaultKeepsService.increaseKeeps(keep)
        } catch (error) {
          Pop.toast('Did not increase Keeps', 'error')
        }
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
