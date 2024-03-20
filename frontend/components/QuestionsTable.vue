<script setup lang="ts">
import type { FormError, FormSubmitEvent } from '#ui/types'

const isModalOpen = ref(false);
const isSlideOpen = ref(false);

const createQuestionState = reactive({
  question: '',
  answer: '',
  tag: ''
});

const modalDisplayState = reactive({
  question: '',
  tag: ''
});

const answerState = reactive({
  userAnswer: ''
});

const openModalWithRowData = (row: any) => {
  isModalOpen.value = true;
  answerState.userAnswer = '';
  modalDisplayState.question = row.question;
  modalDisplayState.tag = row.tag;
};

const resetCreateQuestionState = () => {
  createQuestionState.question = '';
  createQuestionState.answer = '';
  createQuestionState.tag = '';
};

async function onSubmitCreateQuestion(event: FormSubmitEvent<any>) {
  isSlideOpen.value = false;
  resetCreateQuestionState()
}

async function onSubmitAnswer(event: FormSubmitEvent<any>) {
  isModalOpen.value = false;
}

const validateCreateQuestion = (state: any): FormError[] => {
  const errors = []
  if (!state.question) errors.push({ path: 'question', message: 'Required' })
  if (!state.answer) errors.push({ path: 'answer', message: 'Required' })
  if (!state.tag) errors.push({ path: 'tag', message: 'Required' })
  return errors
}

const validateUserAnswer = (state: any): FormError[] => {
  const errors = []
  if (!state.userAnswer) errors.push({ path: 'userAnswer', message: 'Required' })
  return errors
}

const columns = [{
  key: 'question',
  label: 'Question'
}, {
  key: 'category',
  label: 'Category'
}, {
  key: 'tag',
  label: 'Tag'
}, {
  key: 'actions'
}]

const questions = [{
  id: 1,
  question: 'Lindsay Walton',
  category: 'Front-end Developer',
  tag: 'lindsay.walton@example.com',
}, {
  id: 2,
  question: 'Courtney Henry',
  category: 'Designer',
  tag: 'courtney.henry@example.com',
}, {
  id: 3,
  question: 'Tom Cook',
  category: 'Director of Product',
  tag: 'tom.cook@example.com',
}, {
  id: 4,
  question: 'Whitney Francis',
  category: 'Copywriter',
  tag: 'whitney.francis@example.com',
}, {
  id: 5,
  question: 'Leonard Krasner',
  category: 'Senior Designer',
  tag: 'leonard.krasner@example.com',
}, {
  id: 6,
  question: 'Floyd Miles',
  category: 'Principal Designer',
  tag: 'floyd.miles@example.com',
}]

const q = ref('')

const filteredRows = computed(() => {
  if (!q.value) {
    return questions
  }

  return questions.filter((question) => {
    return Object.values(question).some((value) => {
      return String(value).toLowerCase().includes(q.value.toLowerCase())
    })
  })
})

</script>

<template>
  <UContainer>
  <div class="flex px-3 py-3.5 border-b border-gray-200 dark:border-gray-700">
    <p class="font-medium mr-2">Today's questions for review</p>
    <UInput v-model="q" placeholder="Filter questions..." />
    <UButton label="Add Question" @click="isSlideOpen = true" class="ml-2"/>
  </div>
  <USlideover v-model="isSlideOpen">
    <UCard class="flex flex-col flex-1" :ui="{ body: { base: 'flex-1' }, ring: '', divide: 'divide-y divide-gray-100 dark:divide-gray-800' }">
      <template #header>
        <span class="font-medium">Add Question</span>
      </template>

      <UForm :validate="validateCreateQuestion" :state="createQuestionState" class="space-y-4" @submit="onSubmitCreateQuestion">
        <UFormGroup label="Question" name="question">
          <UInput v-model="createQuestionState.question" />
        </UFormGroup>
        <UFormGroup label="Answer" name="answer">
          <UInput v-model="createQuestionState.answer"/>
        </UFormGroup>
        <UFormGroup label="Tag" name="tag">
          <UInput v-model="createQuestionState.tag"/>
        </UFormGroup>

        <UButton type="submit">
          Add
        </UButton>
      </UForm>
    </UCard>
  </USlideover>
  <UTable :rows="filteredRows" :columns="columns">
    <template #actions-data="{ row }">
      <UButton variant="soft" label="Answer" @click="() => openModalWithRowData(row)" />
    </template>
  </UTable>
  <UModal v-model="isModalOpen">
    <UCard>
      <UForm :validate="validateUserAnswer" :state="answerState" class="space-y-4" @submit="onSubmitAnswer">
        <p class="font-medium">{{ modalDisplayState.question }}</p>
        <UBadge color="blue" variant="subtle">{{ modalDisplayState.tag }}</UBadge>
        <UDivider/>
        <UFormGroup label="Answer" name="userAnswer">
          <UInput v-model="answerState.userAnswer" />
        </UFormGroup>
        <UButton variant="soft" type="submit">
          Submit Answer
        </UButton>
      </UForm>
    </UCard>
  </UModal>
  </UContainer>
</template>

