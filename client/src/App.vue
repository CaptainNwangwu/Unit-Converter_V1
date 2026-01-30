<script setup>
import { ref, watch } from 'vue'
import { useTheme } from 'vuetify'
import ConversionForm from '../components/ConversionForm.vue'

const theme = useTheme()
const currentMode = ref('length')

const modes = [
  { value: 'length', label: 'Length', icon: 'mdi-ruler', color: '#1867C0' },
  { value: 'temperature', label: 'Temperature', icon: 'mdi-thermometer', color: '#D32F2F' },
  { value: 'weight', label: 'Weight', icon: 'mdi-weight', color: '#388E3C' }
]

// Update theme when mode changes
watch(currentMode, (newMode) => {
  const mode = modes.find(m => m.value === newMode)
  if (mode) {
    theme.themes.value.light.colors.primary = mode.color
  }
})
</script>

<template>
  <v-app>
    <v-app-bar color="primary" elevation="2">
      <v-toolbar-title class="text-h5 ml-4">
        <v-icon icon="mdi-calculator-variant" class="mr-2"></v-icon>
        Unit Converter
      </v-toolbar-title>
    </v-app-bar>

    <v-main>
      <v-container>
        <v-tabs
          v-model="currentMode"
          color="primary"
          align-tabs="center"
          class="mb-8"
        >
          <v-tab
            v-for="mode in modes"
            :key="mode.value"
            :value="mode.value"
          >
            <v-icon :icon="mode.icon" class="mr-2"></v-icon>
            {{ mode.label }}
          </v-tab>
        </v-tabs>

        <v-container class="d-flex align-center justify-center">
          <ConversionForm :mode="currentMode" />
        </v-container>
      </v-container>
    </v-main>
  </v-app>
</template>

<style scoped></style>
