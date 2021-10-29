<script setup lang="ts">
import { useMainStore } from "@/stores/main";
import { useReCaptcha } from "vue-recaptcha-v3";
import axios from "@/utils/axios";

const main = useMainStore();
const router = useRouter();
const { t } = useI18n();
const { executeRecaptcha, recaptchaLoaded } = useReCaptcha();

const captcha = async () => {
  await recaptchaLoaded();

  const token = await executeRecaptcha("case");

  const req = await axios.post("captcha", undefined, {
    headers: {
      ["X-ReCAPTCHA-V3"]: token,
    },
  });

  console.log({ req: req.data });
};
</script>

<template>
  <ion-page>
    <ion-toolbar>
      <ion-buttons slot="start">
        <ion-back-button></ion-back-button>
      </ion-buttons>
      <ion-title>{{ t("my-cases.title") }} </ion-title>
    </ion-toolbar>
    <ion-content class="ion-padding">
      <ion-list>
        <ion-item
          v-for="currentCase in main.cases"
          :key="currentCase.id"
          @click="router.push(`/praj/${currentCase.id}`)"
        >
          <status-indicator :status="currentCase.status"> </status-indicator>
          <ion-label>
            <h3>{{ currentCase.id }}</h3>
            <p>{{ currentCase.category.name }}</p>
          </ion-label>
        </ion-item>
      </ion-list>

      <ion-button @click="captcha">Execute Captcha</ion-button>
    </ion-content>
  </ion-page>
</template>
