<template>
  <div @click="changeActiveKeep(keep), increaseViews(keep)" class="component card grow border border-dark rounded" data-toggle="modal" :data-target="'#modal' + keep.id">
    <img class="card-img" :src="keep.img" alt="">
    <div class="card-img-overlay d-flex justify-content-between align-items-end">
      <h4 class="card-title text-light text-shadow">
        {{ keep.name }}
      </h4>
      <!-- <router-link :to="{name: 'ProfilePage', params:{id: keep.creator.id}}"> -->
      <img class="rounded-circle profileImage" :src="keep.creator.picture" alt="">
      <!-- </router-link> -->
    </div>
  </div>
  <!-- NOTE Keep Details Modal -->
  <div class="modal fade"
       :id="'modal' + keep.id"
       tabindex="-1"
       role="dialog"
       aria-labelledby="modelTitleId"
       aria-hidden="true"
  >
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <div class="modal-body">
          <div class="row m-0 w-100 d-flex">
            <div class="col-md-6 p-0 d-flex align-items-center">
              <img class="w-100" :src="keep.img" alt="">
            </div>
            <div class="col-md-6 p-0">
              <div class="row m-0 w-100 d-flex justify-content-center">
                <div class="col-md-12 p-0 d-flex justify-content-end">
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="col-md-12 p-0 d-flex justify-content-around">
                  <div class="row m-0 w-100">
                    <div class="col-md-6 p-0 text-center">
                      <i class="fas fa-eye"></i>
                      <p>{{ keep.views }}</p>
                    </div>
                    <div class="col-md-6 p-0 text-center">
                      <i class="fab fa-kickstarter"></i>
                      <p>{{ keep.keeps }}</p>
                    </div>
                  </div>
                </div>
                <div class="col-md-12 p-0 text-center">
                  <h5>{{ keep.name }}</h5>
                </div>
                <div class="col-md-12 col-8 p-2">
                  <p>{{ keep.description }}</p>
                </div>
                <div class="col-md-12 col-8 p-0 d-flex justify-content-between">
                  <div class="row m-0 w-100 my-4">
                    <div class="col-md-12 col-12 p-0 d-flex justify-content-center dropdown">
                      <button class="btn btn-primary dropdown-toggle"
                              type="button"
                              id="dropdownMenu2"
                              data-toggle="dropdown"
                              aria-haspopup="true"
                              aria-expanded="false"
                      >
                        Add to Vault
                      </button>
                      <div class="col-md-12 col-12 w-100 dropdown-menu p-0" aria-labelledby="dropdownMenu2">
                        <VaultDropDown v-for="v in accountVaults" :key="v.id" :vault="v" />
                      </div>
                    </div>
                    <!-- <router-link :to="{name: 'Profile', params:{id: keep.creator.id}}"> -->
                    <div class="col-md-12 col-12 p-2 flex-wrap my-2 ">
                      <div class="row m-0 w-100 d-flex justify-content-between">
                        <div class="col-md-3 col-3 p-0">
                          <img @click="pushtoProfilePage(keep.creator.id, keep.id)" :src="keep.creator.picture" class="w-100 rounded-circle " :alt="keep.creator.name" :title="keep.creator.name">
                        </div>
                        <div class="col-md-8 col-8 p-0 text-wrap text-break">
                          <p>{{ keep.creator.name }}</p>
                        </div>
                      </div>
                    </div>
                  </div>
                  <!-- </router-link> -->
                </div>
                <div class="col-md-12 p-0 justify-content-center d-flex">
                  <i @click="deleteKeep(keep.id)" class="fas fa-trash-alt   red fa-2x" v-if="keep.creatorId === account.id"></i>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import $ from 'jquery'
import { router } from '../router'
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { accountService } from '../services/AccountService'
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Notifier'
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {
      async deleteKeep(id) {
        try {
          await keepsService.deleteKeep(id)
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      pushtoProfilePage(id, keepId) {
        router.push(`/profiles/${id}`)
        $('#modal' + keepId).modal('hide')
      },
      async increaseViews(keep) {
        await keepsService.increaseViews(keep)
      },
      async changeActiveKeep(keep) {
        AppState.activeKeep = keep
      },

      accountVaults: computed(() => AppState.accountVaults),
      account: computed(() => AppState.account)
    }
  }
}

</script>

<style lang="scss" scoped>
.text-shadow {
  text-shadow: black 1px 1px 1px;
}

.profileImage {
  width: 20%;
}

.grow {
transition: all .3s ease-in-out;
}
.grow:hover {
transform: scale(1.02);
cursor: pointer;
}
.rounded {
  border-radius: 10%;
}
.red {
  color: red;
}

</style>
