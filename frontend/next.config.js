const dotenv = require('dotenv');
dotenv.config();

module.exports = {
  publicRuntimeConfig: {
    apiUrl: process.env.API_URL
  }
};
