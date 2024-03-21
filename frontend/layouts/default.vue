<script setup lang="ts">
const links = [{
  label: 'Home',
  icon: 'i-heroicons-home',
  to: '/'
}, {
  label: 'Card Management',
  icon: 'i-heroicons-command-line',
  to: '/management',
}]

const colorMode = useColorMode()
const isDark = computed({
  get () {
    return colorMode.value === 'dark'
  },
  set () {
    colorMode.preference = colorMode.value === 'dark' ? 'light' : 'dark'
  }
})

</script>

<template>
  <ClientOnly>
  <UPageHeader
        title="Leitner System"
        description="A simple app to implement the leitner system"
        :links="[{ label: 'GitHub', color: 'white', to: 'https://github.com/xororist/leitner-system', target: '_blank',},
                 { label: 'Leitner System', to: 'https://en.wikipedia.org/wiki/Leitner_system', target: '_blank'}]"
    />
    <UButton
      :icon="isDark ? 'i-heroicons-sun-20-solid' : 'i-heroicons-moon-20-solid'"
      color="gray"
      variant="ghost"
      aria-label="Theme"
      label="Theme"
      @click="isDark = !isDark"
  />
    <UHorizontalNavigation :links="links" class="border-b border-gray-200 dark:border-gray-800" />
  <NuxtPage />
  </ClientOnly>
</template>
