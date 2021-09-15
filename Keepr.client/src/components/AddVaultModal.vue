<template>
  <!-- NOTE Modal for adding a Vault -->
  <div class="modal fade"
       id="createVaultModal"
       tabindex="-1"
       role="dialog"
       aria-labelledby="modelTitleId"
       aria-hidden="true"
  >
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">
            Create New Vault
          </h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="createVault">
            <div class="form-group">
              <label for="name">Name</label>
              <input type="text"
                     v-model="state.newVault.name"
                     placeholder="Name..."
                     id="name"
                     name="name"
                     class="form-control"
              >
              <label for="description">Description</label>
              <textarea v-model="state.newVault.description"
                        placeholder="Description..."
                        class="form-control"
                        name="description"
                        id="description"
                        rows="3"
                        minlength="3"
                        required
              ></textarea>
              <label for="img">Image</label>
              <input type="text"
                     v-model="state.newVault.img"
                     placeholder="Image URL..."
                     id="img"
                     name="img"
                     class="form-control"
              >
              <label for="isPrivate">Is this Private?</label>
              <input type="checkbox"
                     v-model="state.newVault.isPrivate"
                     placeholder="checkbox for isPrivate"
                     id="isPrivate"
                     name="isPrivate"
                     class="form-control"
              >
            </div>
            <button type="button" class="btn btn-secondary" data-dismiss="modal">
              Close
            </button>
            <button type="submit" class="btn btn-primary">
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
import { vaultsService } from '../services/VaultsService'
import Pop from '../utils/Notifier'
import { reactive } from '@vue/reactivity'
export default {
  name: 'Component',
  setup() {
    const state = reactive({
      newVault: {}
    })
    return {
      state,
      async createVault() {
        try {
          await vaultsService.createVault(state.newVault)
          $('#createVaultModal').modal('hide')
          Pop.toast('You Created A Vault!', 'success')
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
