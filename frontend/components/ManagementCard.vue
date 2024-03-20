<script setup lang="ts">
import type { FormError, FormSubmitEvent } from '#ui/types'
import axios from "axios";

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
  console.log(createQuestionState)
  isSlideOpen.value = false;
  try {
    const response = await axios.post(`http://localhost:4321/cards`, createQuestionState);
    console.log('Card created with ID:', response.data);
    resetCreateQuestionState()
    await fetchAllQuestions()
  } catch (error) {
    console.error('Error creating card:', error);
  }
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
},{
  key: 'answer',
  label: 'Answer'
}, {
  key: 'actions'
}]


const q = ref('')

const filteredRows = computed(() => {
  if (!q.value) {
    return questions.value
  }

  return questions.value.filter((question) => {
    return Object.values(question).some((value) => {
      return String(value).toLowerCase().includes(q.value.toLowerCase())
    })
  })
})

const questions = ref([])

async function fetchAllQuestions() {
  try {
    const response = await axios.get('http://localhost:4321/cards');
    console.log(response.data);
    questions.value = response.data
  } catch (error) {
    console.error('Failed to fetch questions:', error);
  }
}

const onDeleteQuestion = async (questionId: number) => {
  if (!confirm('Are you sure you want to delete this question?')) {
    return;
  }

  try {
    const url = `http://localhost:4321/cards/delete/${questionId}`;
    await axios.delete(url);
    console.log('Question deleted successfully');
    isModalOpen.value = false;
    await fetchAllQuestions()
  } catch (error) {
    console.error('Failed to delete question:', error);
  }
};

onMounted(fetchAllQuestions)

</script>

<template>
    <div class="flex px-3 py-3.5 border-b border-gray-200 dark:border-gray-700">
      <p class="font-medium mr-2">All your questions</p>
      <UInput v-model="q" placeholder="Filter questions..." />
      <UButton label="Add Question" @click="isSlideOpen = true" class="ml-2"/>
    </div>
    <USlideover v-model="isSlideOpen">
      <UCard class="flex flex-col flex-1" :ui="{ body: { base: 'flex-1' }, ring: '', divide: 'divide-y divide-gray-100 dark:divide-gray-800' }">
        <template #header>
          <span>Add Question</span>
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
        <UButton color="blue" variant="soft" label="Edit" @click="() => openModalWithRowData(row)" />
        <UButton color="red" variant="soft" label="Delete" @click="() => onDeleteQuestion(row.id)" class="ml-2" />
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
          <UButton type="submit">
            Submit Answer
          </UButton>
        </UForm>
      </UCard>
    </UModal>
</template>

