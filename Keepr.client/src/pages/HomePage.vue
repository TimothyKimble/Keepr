<template>
  <div class="container-fluid p-1 m-3">
    <div class="card-columns">
      <KeepsCard v-for="k in keeps" :key="k.id" :keep="k" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core'
import Pop from '../utils/Notifier'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { accountService } from '../services/AccountService'
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
