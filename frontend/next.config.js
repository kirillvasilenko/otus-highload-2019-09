const dotenv = require('dotenv');
dotenv.config();

module.exports = {
  publicRuntimeConfig: {
    apiUrl: process.env.API_URL
  },
  serverRuntimeConfig: {
    apiUrl: process.env.API_SERVER_URL
  }
};
