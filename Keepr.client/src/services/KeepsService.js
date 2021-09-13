import { AppState } from '../AppState'
import { api } from './AxiosService'
import { logger } from '../utils/Logger'

class KeepsService {
  async getKeeps() {
    try {
      const res = await api.get('api/keeps')
      logger.log(res.data)
      AppState.keeps = res.data
    } catch (error) {
      logger.error("Couldn't get Keeps", error)
    }
  }
}

export const keepsService = new KeepsService()
