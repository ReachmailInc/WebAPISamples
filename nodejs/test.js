var reachmail = require('./reachmailapi.js');
 
 var api = new reachmail({token: '1RMfibE0bMJ_JZhcgV_fl_BqwJlxxD0-rAvAat8-0_mXZ6hsVJUip16L6AOx4vw2'});
  
  /* This works.
  api.get('/administration/users/current', function (http_code, response) {
  // console.log(http_code); // 200
  console.log('getting current user: ');
  console.log(response); // { AccountId: '4891fbaa-e0fd-4a6b-b3c7-dc9e4ce99d10', AccountKey: 'ZEISGROU1' }
  });
  */
   
   // This does not.
   api.post('/mailings/filtered/4891fbaa-e0fd-4a6b-b3c7-dc9e4ce99d10', "{}", function (http_code, response) {
   if(http_code !== 200){
   console.log(http_code);
   }
   console.log(response);
   });
