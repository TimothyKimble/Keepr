<template>
  <option @click="createVaultKeep()">
    {{ vault.name }}
  </option>
</template>

<script>
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'
import Pop from '../utils/Notifier'
import { logger } from '../utils/Logger'
export default {
  name: 'Component',
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {
      createVaultKeep() {
        logger.log('Connected')
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
