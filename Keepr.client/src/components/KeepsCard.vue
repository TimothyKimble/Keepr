<template>
  <div class="component card grow border border-dark rounded" data-toggle="modal" :data-target="'#modal' + keep.id">
    <img class="card-img" :src="keep.img" alt="">
    <div class="card-img-overlay d-flex justify-content-between align-items-end">
      <h4 class="card-title text-light text-shadow">
        {{ keep.name }}
      </h4>
      <img class="rounded-circle profileImage" :src="keep.creator.picture" alt="">
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
              <div class="row m-0 w-100">
                <div class="col-md-12 p-0 d-flex justify-content-end">
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="col-md-12 p-0 d-flex justify-content-around">
                  <div class="row m-0 w-100">
                    <div class="col-md-4 p-0 text-center">
                      <i class="fas fa-eye"></i>
                      <p>{{ keep.views }}</p>
                    </div>
                    <div class="col-md-4 p-0 text-center">
                      <i class="fab fa-kickstarter"></i>
                      <p>{{ keep.keeps }}</p>
                    </div>
                    <div class="col-md-4 p-0 text-center">
                      <i class="fas fa-share-alt-square"></i>
                      <p>{{ keep.keeps }}</p>
                    </div>
                  </div>
                </div>
                <div class="col-md-12 p-0 text-center">
                  <h5>{{ keep.name }}</h5>
                </div>
                <div class="col-md-12 p-2">
                  <p>{{ keep.description }}</p>
                </div>
                <div class="col-md-12 p-2 d-flex justify-content-between">
                  <div class="btn btn-success">
                    For Vault
                  </div>
                  <img :src="keep.creator.picture" class="profileImage rounded-circle " alt="">
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
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {}
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
  border-radius: 50%;
}

</style>
