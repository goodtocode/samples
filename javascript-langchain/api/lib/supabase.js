import { createClient } from '@supabase/supabase-js'

const supabaseUrl = "URL"
const supabaseAnon = "API-KEY"
export const supabase = createClient(supabaseUrl, supabaseAnon)