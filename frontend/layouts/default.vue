<script setup lang="ts">
const toast = useToast()

const links = [{
  label: 'Home',
  icon: 'i-heroicons-home',
  to: '/'
}, {
  label: 'Card Management',
  icon: 'i-heroicons-command-line',
  to: '/management'
}]
</script>

<template>
  <UPageHeader
        title="Leitner System"
        description="A simple app to implement the leitner system"
        :links="[{ label: 'GitHub', color: 'white', to: 'https://github.com/xororist/leitner-system', target: '_blank',},
                 { label: 'Leitner System', to: 'https://en.wikipedia.org/wiki/Leitner_system', target: '_blank'}]"
    />
    <UHorizontalNavigation :links="links" class="border-b border-gray-200 dark:border-gray-800" />
  <NuxtPage />
</template>

<style scoped>

</style>