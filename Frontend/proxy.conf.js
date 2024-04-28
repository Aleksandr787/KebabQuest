const defaultTarget = 'http://kq-backend:80';

module.exports = [
  {
    context: ['/api/**'],
    target: defaultTarget,
    changeOrigin: true
  }
]
