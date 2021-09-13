<template>
  <div class="row m-0 w-100">
    <KeepsCard v-for="k in keeps" :key="k.id" :keep="k" />
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
.home{
  text-align: center;
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}
</style>
