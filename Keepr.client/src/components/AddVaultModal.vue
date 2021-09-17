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
          <button type="button" class="close" data-dismiss="modal" aria-label="Close" title="Close modal">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="createVault(), reset()">
            <div class="form-group">
              <label for="name">Name</label>
              <input type="text"
                     v-model="state.newVault.name"
                     placeholder="Name..."
                     id="name"
                     name="name"
                     class="form-control"
                     title="Name for vault"
                     required
                     minlength="3"
                     maxlength="50"
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
                        title="Description for vault"
              ></textarea>
              <label for="img">Image</label>
              <input type="text"
                     v-model="state.newVault.img"
                     placeholder="Image URL..."
                     id="img"
                     name="img"
                     class="form-control"
                     required
                     title="Image URL for vault"
              >
              <div class="row d-flex justify-content-center mt-3 m-0 w-100">
                <div class="col-md-3 p-0">
                  <label class="text-center my-2" for="isPrivate">Is this Private?</label>
                </div>
                <div class="col-md-1 p-0">
                  <input type="checkbox"
                         v-model="state.newVault.isPrivate"
                         placeholder="checkbox for isPrivate"
                         id="isPrivate"
                         name="isPrivate"
                         class="form-control"
                         title="Checkbox for making private"
                  >
                </div>
                <div class="col-md-12 p-0 text-center">
                  <p><em>Only you will see this vault</em> </p>
                </div>
              </div>
            </div>
            <div class="row m-0 w-100 d-flex justify-content-between">
              <button type="button" class="btn btn-secondary" data-dismiss="modal" title="Close vault creation modal">
                Close
              </button>
              <button type="submit" class="btn btn-primary" title="Create vault">
                Create
              </button>
            </div>
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
