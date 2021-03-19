<template>
  <div>

    <div class="el-page-header__content">Пользователи</div>
    <div style="margin-top: 10px">
       <el-button @click="createUser()" type="primary">Создать нового пользователя</el-button>
    </div>
    <el-table :data="users" stripe style="width: 100%" v-loading="loadingTable">
      <el-table-column prop="id" label="ID" ></el-table-column>
      <el-table-column prop="firstName" label="Имя">
        <template slot-scope="scope">
          <el-button @click="openUser(scope.row)" type="text" title="Посмотреть отчеты">{{ scope.row.firstName }}</el-button>
        </template>
      </el-table-column>
      <el-table-column prop="lastName" label="Фамилия"></el-table-column>
      <el-table-column prop="middleName" label="Отчество"></el-table-column>
      <el-table-column prop="email" label="Email" ></el-table-column>
      <el-table-column >
        <template slot-scope="scope">
          <el-button @click="editUser(scope.row)">Редактировать</el-button>
        </template>
      </el-table-column>
      <el-table-column >
        <template slot-scope="scope">
          <el-popconfirm @confirm="deleteUser(scope.row.id)" title="Вы уверены?">
            <el-button slot="reference" type="danger">Удалить</el-button>
          </el-popconfirm>
        </template>
      </el-table-column>
    </el-table>

    <UserModal :visible="visible" :handleClose="handleClose" :form="form" />

  </div>
</template>
<script>
  import axios from 'axios';
  import UserModal from '@/components/UserModal.vue';

  export default {
    components: {
      UserModal
    },
    data() {
      return {
        form: { id: null, firstName: null, lastName: null, middleName: null, email: null },
        visible: false,
        users: [],
        loadingTable: false
      }
    },
    methods: {
      editUser(user) {
        this.form = {
          id: user.id,
          firstName: user.firstName,
          lastName: user.lastName,
          middleName: user.middleName,
          email: user.email
        };
        this.visible = true;
      },
      createUser() {
        this.form = { id: null, firstName: null, lastName: null, middleName: null, email: null };
        this.visible = true;
      },
      openUser(user) {
        this.$router.push('/user/' + user.id);
      },
      deleteUser(id) {
        this.form = {id: id};//для удаления достаточно только id
        axios.delete('http://localhost:5001/user', {
                    data: this.form
                }).then(() => {
                    this.handleClose(); 
                });
      },
      loadUsers() {
        this.loadingTable = true;
        axios.get('http://localhost:5001/user').then(users => {
          this.users = users.data;
        }).finally(()=>{
          this.loadingTable = false;
          });
      },
      handleClose() {
        this.visible = false;
        this.loadUsers();
      }
    },
    mounted() {
      this.loadUsers();
    }
  }
</script>
<style scoped>
 
</style>
