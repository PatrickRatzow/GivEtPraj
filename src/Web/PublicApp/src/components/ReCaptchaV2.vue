<script setup lang="ts">
defineEmits<{ verify: unknown; expire: void; fail: void }>();
const reCaptcha = ref<HTMLDivElement | null>(null);
const reCaptchaInstance = ref<string>();

const renderReCaptcha = () => {
  reCaptchaInstance.value = window.grecaptcha.render(reCaptcha.value, {
    sitekey: "6LcvD64cAAAAAExhLEMCgEgWqlkgPYa7cWlt_8c2",
    callback: (response) => emit("verify", response),
    "expired-callback": () => emit("expire"),
    "error-callback": () => emit("fail"),
  });
};

onMounted(async () => {
  if (window.grecaptcha == null) {
    await new Promise((resolve) => {
      window.onReCaptchaLoaded = function () {
        resolve();
      };

      const scriptTag = window.document.createElement("script");
      scriptTag.setAttribute("src", "https://www.google.com/recaptcha/api.js?onload=onReCaptchaLoaded&render=explicit");

      window.document.head.appendChild(scriptTag);
    });
  }
  await renderReCaptcha();
});
</script>

<template>
  <div>
    <div ref="reCaptcha"></div>
  </div>
</template>

<style scoped></style>
