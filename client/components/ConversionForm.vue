<!-- /client/components/ConversionCard.vue -->
<template>
  <v-card width="450" class="mx-auto" elevation="8">
    <v-card-title class="text-h5 bg-primary text-white pa-4">
      <v-icon :icon="modeConfig.icon" class="mr-2"></v-icon>
      {{ modeConfig.title }}
    </v-card-title>

    <!-- Show form when no result is present -->
    <v-card-text v-if="!result" class="pa-6">
      <v-form @submit.prevent="handleConvert">
        <v-text-field
          v-model="measurement"
          label="Enter measurement"
          type="number"
          variant="outlined"
          density="comfortable"
          required
          class="mb-2"
        ></v-text-field>

        <v-select
          v-model="fromUnit"
          :items="availableUnits"
          label="From Unit"
          variant="outlined"
          density="comfortable"
          required
          class="mb-2"
        ></v-select>

        <v-icon icon="mdi-arrow-down" class="d-block mx-auto mb-2" size="large"></v-icon>

        <v-select
          v-model="toUnit"
          :items="availableUnits"
          label="To Unit"
          variant="outlined"
          density="comfortable"
          required
          class="mb-4"
        ></v-select>

        <v-btn
          type="submit"
          color="primary"
          size="large"
          block
          prepend-icon="mdi-equal"
        >
          Convert
        </v-btn>
      </v-form>
    </v-card-text>

    <!-- Show result when conversion is complete -->
    <v-card-text v-else class="pa-6 text-center">
      <div class="text-overline mb-2">Result</div>
      <div class="text-h4 mb-4 primary--text">
        {{ measurement }}{{ fromUnitSymbol }} = {{ result }}{{ toUnitSymbol }}
      </div>
      <v-btn
        @click="reset"
        color="secondary"
        size="large"
        block
        prepend-icon="mdi-refresh"
      >
        Reset
      </v-btn>
    </v-card-text>
  </v-card>
</template>


<script setup>
  import { ref, watch, computed } from 'vue'
  import { API_BASE_URL } from '@/config'

  const props = defineProps({
    mode: {
      type: String,
      required: true,
      validator: (value) => ['length', 'temperature', 'weight'].includes(value)
    }
  })

  const measurement = ref('')
  const fromUnit = ref('')
  const toUnit = ref('')

  const fromUnitSymbol = ref(null)
  const toUnitSymbol = ref(null)
  const result = ref(null)
  const availableUnits = ref([]) // Store fetched units

  const modeConfig = computed(() => {
    const configs = {
      length: { icon: 'mdi-ruler', title: 'Length Converter' },
      temperature: { icon: 'mdi-thermometer', title: 'Temperature Converter' },
      weight: { icon: 'mdi-weight', title: 'Weight Converter' }
    }
    return configs[props.mode]
  })

  // Fetch units based on current mode
  async function fetchUnits() {
    const res = await fetch(`${API_BASE_URL}/convert/${props.mode}/units`)
    availableUnits.value = await res.json()
    // Reset form when mode changes
    reset()
  }

  // Watch for mode changes and fetch new units
  watch(() => props.mode, fetchUnits, { immediate: true })

  /**
   * Handles form submission and converts the measurement
   * using the backend API for the current mode.
   * @remarks
   * This function is called when the form is submitted.
   * It fetches the backend API with the given measurement and unit
   * values and updates the result value with the converted measurement.
   */
  async function handleConvert() {
    console.log('Converting:', measurement.value, fromUnit.value, "to", toUnit.value, "in mode:", props.mode)
      const res = await fetch(`${API_BASE_URL}/convert/${props.mode}`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify ({
          Value: measurement.value,
          FromUnit: fromUnit.value,
          ToUnit: toUnit.value
        })
      })

    const data = await res.json()
    result.value = data.result
    fromUnitSymbol.value = data.fromUnitSymbol
    toUnitSymbol.value = data.toUnitSymbol
    }

  /**
   * Resets the conversion form state.
   * @remarks
   * This function is called when the "Reset" button is clicked.
   * It resets the result value to null and clears the measurement and unit
   * values.
   */
  function reset() {
    result.value = null
    measurement.value = ''
    fromUnit.value = ''
    toUnit.value = ''
  }
</script>
