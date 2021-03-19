<template>
   <div>
      <el-dialog title="Отчет" :visible="visible" width="30%" :before-close="handleClose">
        <p v-if="this.errors" class="errors">
            <b>Пожалуйста исправьте указанные ошибки:</b>
            <ul>
            <li v-for="error in errors" :key="error">{{ error }}</li>
            </ul>
        </p>
         <el-form  label-width="100px" :model="form">
            <el-form-item label="Описание" >
               <el-input type="textarea" :rows="5" class="descripton" v-model="form.description"></el-input>
            </el-form-item>
            <el-form-item label="Дата начала">
               <el-date-picker v-model="form.startDate" type="datetime" placeholder="Pick a day" :clearable="false"></el-date-picker>
            </el-form-item>
            <el-form-item label="Часы">
                 <el-input-number v-model="form.hours" :precision="2" :step=0.1 :max=10 :min=0.05 ></el-input-number>
            </el-form-item>
         </el-form>
         <span slot="footer" class="dialog-footer">
            <el-button @click="handleClose()">Cancel</el-button>
            <el-button type="primary" @click="sendForm()">Confirm</el-button>
         </span>
      </el-dialog>
      
   </div>
</template>
<script>
import axios from 'axios';

export default {
    props: {
        visible: Boolean,
        handleClose: Function,
        form: Object,
        errors: []
    },
    methods: {
        checkInputs() {
            for (var prop in this.form) {
                if (this.form[prop] == "")
                    this.form[prop] = null;
            }
            return true;
        },
        sendForm() {
            if (!this.checkInputs())
                return;
            if (this.form.id) { // если есть id значит мы редактируем
                axios.put('/api/report',
                    JSON.stringify(this.form), {
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    }
                ).then(() => { //если все хорошо закрываем модалку
                    this.handleClose();
                }).catch((error) => {
                    let msg = error.response.data
                    this.errors = [];
                    this.errors.push(msg);
                });
            } else { // если нет id значит мы создаем
                this.form.id = 0;
                axios.post("/api/report",
                    JSON.stringify(this.form), {
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    }
                ).then(() => {
                    this.handleClose();
                }).catch((error) => {
                    let msg = error.response.data;
                    this.errors = [];
                    this.errors.push(msg);
                });
            }
        }
    }
}
</script>

<style scoped>
.errors{
    color: red;
    margin: -20px 0 20px 20px;
}
</style>