<template>
  <div>
    <div class="p-4 bg-gray-200">
      <router-link class="mr-4" to="/">Home</router-link>
      <router-link class="mr-4" to="/about">About</router-link>
      <router-link class="mr-4" to="/pictures">Pictures</router-link>
      <router-link class="mr-4" to="/create-case">Create Case</router-link>
    </div>
    <div class="p-2">
      <router-view />
    </div>
  </div>
</template>

<style>
.router-link-exact-active {
  color: rgb(59, 130, 246);
}
</style>
