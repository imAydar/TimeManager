<template>
  <div>

    <el-page-header @back="goBack" :content="'Отчеты пользователя ' + $route.query.name"></el-page-header>
     <div style="margin-top: 10px">
       <el-button @click="createReport($route.params.id)" type="primary">Создать новый отчет</el-button>
    </div>
    <el-table :data="reports" stripe style="width: 100%" v-loading="loadingTable">
      <el-table-column prop="id" label="ID" width="55%"></el-table-column>
      <el-table-column prop="startDate" label="Начало">
        <template slot-scope="scope">
          <el-date-picker v-model="scope.row.startDate" type="datetime" :readonly="true"></el-date-picker>
        </template>
      </el-table-column>
      <el-table-column prop="description" label="Описание">
         <template slot-scope="scope">
           <el-button @click="editReport(scope.row)" type="text">{{scope.row.description.length > 50 ? 
             scope.row.description.substring(0,50) + "..." : 
             scope.row.description}}</el-button>
         </template>
      </el-table-column>
      <el-table-column prop="hours" label="Часов" width="100%" ></el-table-column>
      <el-table-column width="150px">
        <template slot-scope="scope">
          <el-button @click="editReport(scope.row)">Редактировать</el-button>
        </template>
      </el-table-column>
      <el-table-column width="140px">
        <template slot-scope="scope">
          <el-popconfirm @confirm="deleteReport(scope.row.id)" title="Вы уверены?">
            <el-button slot="reference" type="danger">Удалить</el-button>
          </el-popconfirm>
        </template>
      </el-table-column>
    </el-table>

    <ReportModal :visible="visible" :handleClose="handleClose" :form="form" />

  </div>
</template>

<script>
import { http } from '@/helpers/http-common';
import ReportModal from '@/components/ReportModal.vue';

export default {
  components: {
    ReportModal
  },
  data() {
    return {
      reports: [],
      loadingTable: false,
      visible: false,
      form: {
        id: null,
        startDate: null,
        description: null,
        userId: null,
        hours: null
      }
    }
  },
  methods: {
    goBack() {
      window.history.length > 1 ? this.$router.go(-1) : this.$router.push('/')
    },
    loadReports() {
      this.loadingTable = true;
      http.get('report/GetReportsByUserId/' + this.$route.params.id)
        .then(reports => {
          this.reports = reports.data;
        }).finally(() => {
          this.loadingTable = false;
        });
    },
    createReport(userId) {
      console.log(this.$route);
      this.form = {
        id: 0,
        startDate: new Date(),
        description: null,
        userId: userId,
        hours: null
      };
      this.visible = true;
    },
    editReport(report) {
      this.visible = true;
      this.form = {
        id: report.id,
        startDate: report.startDate,
        description: report.description,
        userId: report.userId,
        hours: report.hours
      };
    },
    deleteReport(reportId) {
      this.form = {
        id: reportId
      }; //для удаления достаточно только id
      http.delete('report', {
        data: this.form
      }).then(() => {
        this.handleClose();
      });
    },
    handleClose() {
      this.visible = false;
      this.loadReports();
    }
  },
  mounted() {
    this.loadReports();
  }
}
</script>
