<template>
  <!-- NOTE Modal for adding a Keep -->
  <div class="modal fade"
       id="createKeepModal"
       tabindex="-1"
       role="dialog"
       aria-labelledby="modelTitleId"
       aria-hidden="true"
  >
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">
            Create New Keep
          </h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close" title="close modal">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="createKeep">
            <div class="form-group">
              <label for="name">Name</label>
              <input type="text"
                     v-model="state.newKeep.name"
                     placeholder="name..."
                     id="name"
                     name="name"
                     class="form-control"
                     required
                     minlength="3"
                     maxlength="50"
                     title="Name for Keep"
              >
              <label for="description">Description</label>
              <textarea v-model="state.newKeep.description"
                        placeholder="Description..."
                        class="form-control"
                        name="description"
                        id="description"
                        rows="3"
                        minlength="3"
                        required
                        title="Description for Keep"
              ></textarea>
              <label for="img">Image</label>
              <input type="text"
                     v-model="state.newKeep.img"
                     placeholder="image URL..."
                     id="img"
                     name="img"
                     class="form-control"
                     required
                     minlength="10"
                     title="Image URL"
              >
            </div>
            <button type="button" class="btn btn-secondary" data-dismiss="modal" title="Close Keep Creation Modal">
              Close
            </button>
            <button type="submit" class="btn btn-primary" title="Create Keep Button">
              Create
            </button>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import $ from 'jquery'
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Notifier'
import { reactive } from '@vue/reactivity'
export default {
  name: 'Component',
  setup() {
    const state = reactive({
      newKeep: {}
    })
    return {
      state,
      async createKeep() {
        try {
          await keepsService.createKeep(state.newKeep)
          $('#createKeepModal').modal('hide')
          Pop.toast('You Created A Keep!', 'success')
        } catch (error) {
          Pop.toast(error)
        }
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
