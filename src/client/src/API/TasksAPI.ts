export function GetTasks(){
    fetch("https://localhost:7269/",
    {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwOi8vMTI3LjAuMC4xOjgwODAvIiwiaWF0IjoxNzA4Njg4NDEyLCJleHAiOjE3MTM4NzI0MTIsImlkIjoxfQ.t-fmo4LBT0CdwZsE4Kbd6KKT_ZvAn_NJLLJXlPfI5zU"
        }
    }).then(res => res.json())
    .then(data => console.log(data));
}