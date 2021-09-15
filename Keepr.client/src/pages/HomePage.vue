<template>
  <div class="container-fluid p-1 mt-1">
    <div class="card-columns">
      <KeepsCard v-for="k in keeps" :key="k.id" :keep="k" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted, ref } from '@vue/runtime-core'
import Pop from '../utils/Notifier'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
export default {
  setup() {
    onMounted(async() => {
      try {
        await keepsService.getKeeps()
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped lang="scss">

</style>
