<template>
  <div class="component col-md-4 p-3">
    <div class="row m-0 w-100" :style="{ backgroundImage: `url(${keep.img})`, minWidth: 100 +`%`, minHeight: 250 + `px`, backgroundSize: `cover`, backgroundPosition: `center`}">
      <div class="col-md-8 p-0 d-flex justify-content-end align-items-end text-light text-shadow">
        {{ keep.name }}
      </div>
      <div class="col-md-4 p-0 d-flex justify-content-end align-items-end px-3">
        <img class="profileImage rounded-circle" :src="keep.creator.picture" alt="">
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
  width: 50%;
}
</style>
