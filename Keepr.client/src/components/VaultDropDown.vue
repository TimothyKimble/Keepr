<template>
  <button @click="createVaultKeep(vault)" class="dropdown-item">
    {{ vault.name }}
  </button>
</template>

<script>
import { reactive } from '@vue/runtime-core'
import { AppState } from '../AppState'
import Pop from '../utils/Notifier'
import { vaultKeepsService } from '../services/VaultKeepsService'
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
          await vaultKeepsService.createVaultKeep(state.newVaultKeep)
          Pop.toast('Created VaultKeep', 'success')
        } catch (error) {
          Pop.toast('Did not create vaultKeep', 'error')
        }
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
