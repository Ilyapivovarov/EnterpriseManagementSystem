import React from "react";
import {Box, Button, ButtonGroup, Paper, Typography} from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import Link from "../../components/Link/Link";
import UserSelector from "../../components/UserSelector/UserSelector";
import {useGetTaskByIdQuery} from "../../services/taskService";
import {useParams} from "react-router-dom";
import TaskStatusSelector from "../../components/TaskStatusSelector/TaskStatusSelect";

// const dataMock: dataDto = {
//   id: 1,
//   guid: "ca851edd-cafd-4ce2-9daa-1ee323502f96",
//   name: "Test data",
//   description: "Show data",
//   created: new Date(),
//   executor: {
//     id: 1,
//     emailAddress: "admin@admin.com",
//     firstName: "Admin",
//     lastName: "Admin",
//     guid: "036e58dc-17c3-4c60-93a8-572d6089e3f1",
//   },
//   author: {
//     id: 1,
//     emailAddress: "admin@admin.com",
//     firstName: "Admin",
//     lastName: "Admin",
//     guid: "036e58dc-17c3-4c60-93a8-572d6089e3f1",
//   },
//
//   inspector: {
//     id: 1,
//     emailAddress: "admin@admin.com",
//     firstName: "Admin",
//     lastName: "Admin",
//     guid: "036e58dc-17c3-4c60-93a8-572d6089e3f1",
//   },
//   observers: [
//     {
//       id: 1,
//       emailAddress: "admin@admin.com",
//       firstName: "Admin",
//       lastName: "Admin",
//       guid: "036e58dc-17c3-4c60-93a8-572d6089e3f1",
//     },
//   ],
//   status: {
//     id: 2,
//     name: "Active",
//     guid: "84d8a7a6-c2be-41c4-90e1-36a7969ab3f1",
//   },
// };

const dataPage = () => {
  const {id} = useParams();
  // console.log(id);
  const {data, isLoading, isSuccess, error} = useGetTaskByIdQuery(id!);

  if (isLoading)
    return <>Loading</>

  if (!isSuccess)
    return <>{error}</>

  return (
      <Paper
          sx={{
            padding: 2,
            display: "flex",
            flexDirection: "column",
            height: "100%",
          }}
      >
        <Box padding={1} display={"flex"} justifyContent={"space-between"}>
          <Typography fontSize={14} paddingLeft={1}>
            data-{data.id} created by{" "}
            <Link to={`/users/${data.author.guid}`}>
              {data.author.firstName} {data.author.lastName}{" "}
            </Link>
            {data.created}
          </Typography>
          <Box>
            <ButtonGroup size="small">
              <Button key="edit">
                <EditIcon/>
              </Button>
              <Button key="delete">
                <DeleteIcon/>
              </Button>
            </ButtonGroup>
          </Box>
        </Box>
        <Box padding={1}>
          <Box display={"flex"} justifyContent={"space-between"}>
            <Typography
                paddingBottom={2}
                paddingTop={1}
                variant="h3"
                paddingLeft={1}
            >
              {data.name}
            </Typography>
            <Box display={"flex"} justifyContent={"space-between"}>
              <div style={{marginRight: "5px"}}>
                <UserSelector currentExecutor={data.executor.id}/>
              </div>
              <TaskStatusSelector selectedStatusId={1}/>
            </Box>
          </Box>
          <Typography fontSize={20} paddingLeft={1}>
            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Assumenda,
            cumque eaque fuga illum in molestiae perferendis porro quaerat
            recusandae soluta. Assumenda molestiae quae reiciendis repudiandae
            soluta? Amet assumenda minima minus!
          </Typography>
        </Box>
      </Paper>
  );
};

export default dataPage;
