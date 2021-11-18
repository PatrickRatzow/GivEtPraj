<template>
  <div>
    <div v-show="showCaptcha">
      <re-captcha-v2  />
    </div>
    
    
    <ion-button @click="click">Send</ion-button>
  </div>
</template>

<script setup lang="ts">
import ReCaptchaV2 from "@/components/ReCaptchaV2.vue";
import axios from "@/utils/axios";

var showCaptcha = ref(false);


async function click() {
  console.log("click!");
  try {
    const response = await axios.post("cases", {});

    if (response.status == 400) {
      showCaptcha.value = true;
      console.log(showCaptcha.value);
    }
  }
  catch (e) {
    console.log({e});
    if (e.response.status == 400) {
      showCaptcha.value = true;
      console.log(showCaptcha.value);
    }
  }
  
}
</script>

<style scoped></style>
