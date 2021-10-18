<script setup lang="ts">
import { computed } from "vue";
import { useStore } from "../../store";

const store = useStore();
const caseInCreation = computed(() => store.state.caseInCreation);
</script>

<template>
  <div>
    <h1 v-if="caseInCreation">
      Case in creation!

      <br />

      {{ caseInCreation.category }}
    </h1>
    <div v-else>
      <button @click="store.commit('startCaseCreation')">start</button>
    </div>
  </div>
</template>
